[web] POST /api/ui/tax-agents/  (Dataverse.Api.Controllers.UI.Core.TaxAgentController.Create)  [L50–L57] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaxAgentRepository.Add [L54]
  └─ writes_to TaxAgent [L54]
    └─ reads_from TaxAgents
  └─ uses_service IControlledRepository<TaxAgent>
    └─ method Add [L54]

