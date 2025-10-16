[web] GET /api/ui/tax-agents/  (Dataverse.Api.Controllers.UI.Core.TaxAgentController.GetAll)  [L32–L38] status=200 [auth=Authentication.UserPolicy]
  └─ calls TaxAgentRepository.ReadQuery [L35]
  └─ query TaxAgent [L35]
    └─ reads_from TaxAgents
  └─ uses_service IControlledRepository<TaxAgent>
    └─ method ReadQuery [L35]
      └─ ... (no implementation details available)

