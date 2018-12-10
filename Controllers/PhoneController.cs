using Microsoft.AspNetCore.Mvc;
using NewZap_v2.Models;
using Nexmo.Api;
using System.Linq;


namespace NewZap_v2.Controllers
{
    [Route("api/[controller]")]
    public class PhoneController : Controller
    {
        private readonly PhoneContext _context;

        public PhoneController(PhoneContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Phones.ToList());
        }

        [HttpGet("{id}", Name = "createdNumber")]
        public IActionResult GetById(int id)
        {
            var payload = _context.Phones.FirstOrDefault(x => x.PhoneID == id);
            if (payload == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(payload);
            }
        }

        [HttpPost]
        public IActionResult CreateNumberRequest([FromBody] PhoneData payload)
        {
            if (ModelState.IsValid)
            {
                if (payload.IsExisting(_context))
                {
                    return BadRequest("Number already exists");
                }

                else
                {
                    payload.SaveNumber(_context);
                    if (payload.SaveNumber(_context) == true)
                    {
                        _context.SaveChanges();
                        
                        string valor = PhoneService.GenerateRandomNumber();

                        var client = new Client(creds: new Nexmo.Api.Request.Credentials
                        {
                            ApiKey = "446f0ce7",
                            ApiSecret = "km4BNlHHwAQQlpwq"
                        });


                        var results = client.SMS.Send(request: new SMS.SMSRequest
                        {
                            from = "NewZapp",
                            to = "55" + payload.DDD + payload.PhoneNumber,
                            text = "NewZapp: Authorization Code:" + valor
                        });

                        return new CreatedAtRouteResult("createdNumber", new { id = payload.PhoneID }, payload); //Criando um nova rota, para que quando houver a atualização desejada seremos redirecionados para esta nova rota
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PhoneData payload)
        {
            var upPhone = _context.Phones.Find(id);
            if (upPhone == null)
            {
                return NotFound();
            }

            upPhone.DDD = payload.DDD;
            upPhone.PhoneNumber = payload.PhoneNumber;

            _context.Phones.Update(upPhone);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var code = _context.Phones.Find(id);

            if (code == null)
            {
                return NotFound();
            }

            _context.Phones.Remove(code);
            _context.SaveChanges();
            return Ok();
        }
    }
}