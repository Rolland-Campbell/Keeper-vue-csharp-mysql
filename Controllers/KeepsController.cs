using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;
    public KeepsController(KeepsService ks)
    {
      _ks = ks;
    }
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get()
    {
      try
      {
        return Ok(_ks.Get());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }

    [HttpPost]
    [Authorize]
    public ActionResult<Keep> Post([FromBody] Keep newKeep)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        newKeep.UserId = userId;
        return Ok(_ks.Create(newKeep));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}

// using System;
// using System.Collections.Generic;
// using System.Security.Claims;
// using Keepr.Models;
// using Keepr.Services;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace KeepsController.Controllers
// {
//   [ApiController]
//   [Route("api/[controller]")]

//   public class KeepsController : ControllerBase
//   {
//     private readonly KeepsService _ks;
//     public KeepsController(KeepsService ks)
//     {
//       _ks = ks;
//     }

//     [HttpGet]
//     public ActionResult<IEnumerable<Keep>> Get()
//     {
//       try
//       {
//         return Ok(_ks.Get());
//       }
//       catch (Exception e)
//       {
//         return BadRequest(e.Message);
//       }
//     }

//     [HttpGet("{id}")]
//     public ActionResult<Keep> Get(int id)
//     {
//       try
//       {
//         return Ok(_ks.Get(id));
//       }
//       catch (Exception e)
//       {
//         return BadRequest(e.Message);
//       }
//     }

//     [HttpGet("user")]
//     public ActionResult<IEnumerable<Keep>> GetKeepsByUser()
//     {
//       try
//       {
//         string id = HttpContext.User.FindFirstValue("Id");
//         return Ok(_ks.GetKeepsByUser(id));
//       }
//       catch (Exception e)
//       {
//         return BadRequest(e.Message);
//       }
//     }

//     [Authorize]
//     [HttpPost]
//     public ActionResult<Keep> Create([FromBody]Keep newKeep)
//     {
//       try
//       {
//         newKeep.UserId = HttpContext.User.FindFirstValue("Id");
//         return Ok(_ks.Create(newKeep));
//       }
//       catch (Exception e)
//       {
//         return BadRequest(e.Message);
//       }
//     }

//     [Authorize]
//     [HttpPut("{id}")]
//     public ActionResult<Keep> Edit([FromBody] Keep newKeep, int id)
//     {
//       try
//       {
//         newKeep.Id = id;
//         return Ok(_ks.Edit(newKeep));
//       }
//       catch (Exception e)
//       {
//         return BadRequest(e.Message);
//       }
//     }

//     [Authorize]
//     [HttpDelete("{id}")]
//     public ActionResult<string> Delete(int id)
//     {
//       try
//       {
//         string userId = HttpContext.User.FindFirstValue("id");
//         return Ok(_ks.Delete(id, userId));
//       }
//       catch (Exception e)
//       {
//         return BadRequest(e.Message);
//       }
//     }
//   }
// }