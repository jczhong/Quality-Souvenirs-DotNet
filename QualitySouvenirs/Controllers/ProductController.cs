﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using QualitySouvenirs.Utilities;

namespace QualitySouvenirs.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly List<Category> categories;

        public ProductController(ApplicationContext context)
        {
            _context = context;
            categories = _context.Categories.ToList();
        }

        public async Task<IActionResult> Index(int? id, bool? byId, string sort, string search, int? page, int? minprice, int? maxprice)
        {
            int currentID;
            string currentSort;
            bool currentById;
            ViewData["Categories"] = categories;

            if (id == null)
            {
                currentID = -1;
            }
            else
            {
                currentID = id.Value;
            }

            if (byId == null)
            {
                currentById = false;
            }
            else
            {
                currentById = byId.Value;
            }

            if (sort == null)
            {
                currentSort = "popularity";
            }
            else
            {
                currentSort = sort;
            }


            var souvenirs = from s in _context.Souvenirs
                            select s;

            if (minprice != null)
            {
                ViewData["MinPrice"] = minprice;
                souvenirs = souvenirs.Where<Souvenir>(s => s.Price >= minprice);
            }

            if (maxprice != null)
            {
                ViewData["MaxPrice"] = maxprice;
                souvenirs = souvenirs.Where<Souvenir>(s => s.Price <= maxprice);
            }

            if (currentID != -1)
            {
                souvenirs = souvenirs.Where<Souvenir>(s => s.Category.ID == currentID);
            }

            if (search != null)
            {
                ViewData["SearchString"] = search;
                souvenirs = souvenirs.Where(s => s.Name.Contains(search));
            }

            ViewData["FilterById"] = currentById ? "true" : "false";
            if (currentById)
            {
                ViewData["FilterID"] = currentID;
            }

            ViewData["SortType"] = currentSort;
            var sortViewModel = new SortViewModel();
            sortViewModel.SortType = currentSort;
            ViewData["SortViewModel"] = sortViewModel;

            switch (currentSort)
            {
                case "popularity_desc":
                    souvenirs = souvenirs.OrderBy(s => s.Popularity);
                    break;

                case "popularity":
                    souvenirs = souvenirs.OrderByDescending(s => s.Popularity);
                    break;

                case "price":
                    souvenirs = souvenirs.OrderByDescending(s => s.Price);
                    break;

                case "price_desc":
                    souvenirs = souvenirs.OrderBy(s => s.Price);
                    break;
            }

            int pageSize = 10;

            return View(await PaginatedList<Souvenir>.CreateAsync(souvenirs.AsNoTracking(), page ?? 1, pageSize));
        }

        public async Task<IActionResult> ProductDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs.FindAsync(id);
            if (souvenir == null)
            {
                return NotFound();
            }
            return View(souvenir);
        }
    }
}