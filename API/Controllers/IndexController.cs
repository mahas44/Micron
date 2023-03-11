using Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using API.Validators;

namespace API.Controllers
{
    //[ServiceFilter(typeof(LogFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        public IndexController()
        {

        }
        /// <summary>
        /// Insert a new Game
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        //[LogAttribute]
        [HttpPost]
        public ResultModel<Game> InsertGame(Game model)
        {
            var result = new ResultModel<Game>();
            List<(bool, CustomException)> errorList = ValidateClassProperties.GetValidateResult(model);

            if (errorList.Count > 0)
            {
                result.Exceptions = errorList.Select(li => li.Item2).ToList();
                result.Value = model;
                return result;
            }
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest",
                    Port = AmqpTcpEndpoint.UseDefaultPort,
                    VirtualHost = "/",

                };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "game",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var gameData = JsonConvert.SerializeObject(model);
                    var body = Encoding.UTF8.GetBytes(gameData);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "game",
                        basicProperties: null,
                        body: body);

                    Console.WriteLine($"{model.Name} is Send to the queue");
                }
                return result;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return result;
            }
        }
    }
}
