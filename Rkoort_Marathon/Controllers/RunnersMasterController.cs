using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rkoort_Marathon.Models;

namespace Rkoort_Marathon.Controllers
{
    public class RunnersMasterController : Controller
    {
        private readonly ApplicationDBContext _context;

        public RunnersMasterController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult RegisterRunners(RunnersMaster newRunner)
        {

            List<RunnersMaster> data = _context.RunnersMaster.ToList();

            return View(new AddRunnersViewModel() { NewRunner=newRunner,RegisteredRunners=data});
            
        }


        [HttpPost]
        public async Task<IActionResult> Addrunners( AddRunnersViewModel model)
        {
           // RunnersMaster runner = new RunnersMaster
           // {
             //   FirstName = Request.Form["firstname"],
             //   LastName = Request.Form["lastname"],
           // };

            if(ModelState.IsValid)
            { 
            _context.Add(model.NewRunner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(RegisterRunners));
            }
            return View("RegisterRunners",model.NewRunner);
            
        }

     






        // GET: RunnersMaster
        public async Task<IActionResult> Index()
        {
            return View(await _context.RunnersMaster.ToListAsync());
        }

        // GET: RunnersMaster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var runnersMaster = await _context.RunnersMaster
                .FirstOrDefaultAsync(m => m.id == id);
            if (runnersMaster == null)
            {
                return NotFound();
            }

            return View(runnersMaster);
        }

        // GET: RunnersMaster/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RunnersMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,FirstName,LastName")] RunnersMaster runnersMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(runnersMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(runnersMaster);
        }

        // GET: RunnersMaster/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var runnersMaster = await _context.RunnersMaster.FindAsync(id);
            if (runnersMaster == null)
            {
                return NotFound();
            }
            return View(runnersMaster);
        }

        // POST: RunnersMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,FirstName,LastName")] RunnersMaster runnersMaster)
        {
            if (id != runnersMaster.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(runnersMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RunnersMasterExists(runnersMaster.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(runnersMaster);
        }

        // GET: RunnersMaster/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var runnersMaster = await _context.RunnersMaster
                .FirstOrDefaultAsync(m => m.id == id);
            if (runnersMaster == null)
            {
                return NotFound();
            }

            return View(runnersMaster);
        }

        // POST: RunnersMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var runnersMaster = await _context.RunnersMaster.FindAsync(id);
            _context.RunnersMaster.Remove(runnersMaster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RunnersMasterExists(int id)
        {
            return _context.RunnersMaster.Any(e => e.id == id);
        }
    }
}
