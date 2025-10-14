[web] DELETE /api/ui/tax-agents/{id}  (Dataverse.Api.Controllers.UI.Core.TaxAgentController.Delete)  [L71–L81] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaxAgentRepository.Remove [L78]
  └─ calls TaxAgentRepository.WriteQuery [L74]
  └─ writes_to TaxAgent [L78]
    └─ reads_from TaxAgents
  └─ writes_to TaxAgent [L74]
    └─ reads_from TaxAgents
  └─ uses_service IControlledRepository<TaxAgent>
    └─ method WriteQuery [L74]

