﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using One_Vision.Models;

namespace One_Vision.Controllers
{
    public class PacientesController : Controller
    {
        //private readonly OneVisionDbContext _context;

        //public PacientesController(OneVisionDbContext context)
        //{
        //    _context = context;
        //}
        //[Authorize]
        //// GET: Pacientes
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Pacientes.ToListAsync());
        //}

        //// GET: Pacientes/Details/5
        //public async Task<IActionResult> Details(int id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var paciente = await _context.Pacientes
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (paciente == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(paciente);
        //}

        //// GET: Pacientes/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Pacientes/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ID,Nombre,Edad,Telefono,Correo,Direccion")] Paciente paciente)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(paciente);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(paciente);
        //}

        //// GET: Pacientes/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var paciente = await _context.Pacientes.FindAsync(id);
        //    if (paciente == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(paciente);
        //}

        //// POST: Pacientes/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Edad,Telefono,Correo,Direccion")] Paciente paciente)
        //{
        //    if (id != paciente.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(paciente);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PacienteExists(paciente.ID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(paciente);
        //}

        //// GET: Pacientes/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var paciente = await _context.Pacientes
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (paciente == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(paciente);
        //}

        //// POST: Pacientes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var paciente = await _context.Pacientes.FindAsync(id);
        //    if (paciente != null)
        //    {
        //        _context.Pacientes.Remove(paciente);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool PacienteExists(int id)
        //{
        //    return _context.Pacientes.Any(e => e.ID == id);
        //}
    }
}
