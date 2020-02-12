﻿using System.Collections.Generic;
using System.Threading.Tasks;
using trifenix.agro.db.model.agro.core;
using trifenix.agro.model.external;

namespace trifenix.agro.external.interfaces.entities.core {

    public interface ICostCenterOperations {
        Task<ExtPostContainer<string>> SaveNewCostCenter(string name, string rut, string phone, string email, string webPage, string giro);
        Task<ExtPostContainer<CostCenter>> SaveEditCostCenter(string idCostCenter, string name, string rut, string phone, string email, string webPage, string giro);
        Task<ExtGetContainer<CostCenter>> GetCostCenter(string id);
        Task<ExtGetContainer<List<CostCenter>>> GetCostCenters();
        
    }

}