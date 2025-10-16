[web] DELETE /api/ui/tax-agents/{id}  (Dataverse.Api.Controllers.UI.Core.TaxAgentController.Delete)  [L71–L81] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls TaxAgentRepository.Remove [L78]
  └─ calls TaxAgentRepository.WriteQuery [L74]
  └─ delete TaxAgent [L78]
    └─ reads_from TaxAgents
  └─ write TaxAgent [L74]
    └─ reads_from TaxAgents
  └─ uses_service IControlledRepository<TaxAgent>
    └─ method WriteQuery [L74]
      └─ ... (no implementation details available)

