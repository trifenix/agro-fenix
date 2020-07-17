﻿using Cosmonaut;
using Cosmonaut.Attributes;
using System;
using trifenix.agro.db;
using trifenix.connect.agro.index_model.props;
using trifenix.connect.agro.mdm_attributes;
using trifenix.connect.mdm.enums;

namespace trifenix.connect.agro.model
{

    /// <summary>
    /// el evento fenológico se debería crear una vez al año o copiarse del año anterior.
    /// </summary>
    [SharedCosmosCollection("agro", "PhenologicalEvent")]
    [ReferenceSearchHeader(EntityRelated.PHENOLOGICAL_EVENT, Kind = EntityKind.ENTITY, PathName = "phenological_events")]
    [GroupMenu(MenuEntityRelated.MANTENEDORES, PhisicalDevice.ALL, SubMenuEntityRelated.ORDEN_APLICACION)]
    public class PhenologicalEvent : DocumentBaseName<long>, ISharedCosmosEntity {

        public override string Id { get; set; }


        /// <summary>
        /// Identificador visual 
        /// </summary>
        [AutoNumericSearch(NumRelated.GENERIC_CORRELATIVE)]
        public override long ClientId { get; set; }

        /// <summary>
        /// nombre del evento fenológico.
        /// </summary>
        [StringSearch(StringRelated.GENERIC_NAME)]
        public override string Name { get; set; }

        /// <summary>
        /// fecha de inicio
        /// </summary>
        [DateSearch(DateRelated.START_DATE_PHENOLOGICAL_EVENT)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// fecha fin del evento fenológico.
        /// </summary>
        [DateSearch(DateRelated.END_DATE_PHENOLOGICAL_EVENT)]
        public DateTime EndDate { get; set; }

    }
}