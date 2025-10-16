[web] POST /api/ui/tax-agents/  (Dataverse.Api.Controllers.UI.Core.TaxAgentController.Create)  [L50–L57] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaxAgentRepository.Add [L54]
  └─ insert TaxAgent [L54]
    └─ reads_from TaxAgents
  └─ uses_service IControlledRepository<TaxAgent>
    └─ method Add [L54]
      └─ ... (no implementation details available)

