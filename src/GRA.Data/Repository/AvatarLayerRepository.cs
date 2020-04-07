﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using GRA.Domain.Model;
using GRA.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GRA.Data.Repository
{
    public class AvatarLayerRepository
        : AuditingRepository<Model.AvatarLayer, AvatarLayer>,
        IAvatarLayerRepository
    {
        public AvatarLayerRepository(ServiceFacade.Repository repositoryFacade,
            ILogger<AvatarLayerRepository> logger) : base(repositoryFacade, logger)
        {
        }

        public async Task<ICollection<AvatarLayer>> GetAllAsync(int siteId)
        {
            return await DbSet.AsNoTracking()
               .Where(_ => _.SiteId == siteId)
               .OrderBy(_ => _.GroupId)
               .ThenBy(_ => _.SortOrder)
               .ProjectTo<AvatarLayer>(_mapper.ConfigurationProvider)
               .ToListAsync();
        }

        public async Task<ICollection<AvatarLayer>> GetAllWithColorsAsync(int siteId)
        {
            return await DbSet.AsNoTracking()
                .Where(_ => _.SiteId == siteId)
                .OrderBy(_ => _.GroupId)
                .ThenBy(_ => _.SortOrder)
                .ProjectTo<AvatarLayer>(_mapper.ConfigurationProvider, _ => _.AvatarColors)
                .ToListAsync();
        }

        public Dictionary<string, string> GetNameAndLabelByLanguageId(int layerId, int languageId)
        {
            var layerText = _context.AvatarLayerText
                   .AsNoTracking()
                   .Where(_ => _.AvatarLayerId == layerId && _.LanguageId == languageId)
                   .FirstOrDefault();
            return new Dictionary<string, string>
            {
                { "Name", layerText.Name },
                { "RemoveLabel", layerText.RemoveLabel }
            };
        }
    }
}
