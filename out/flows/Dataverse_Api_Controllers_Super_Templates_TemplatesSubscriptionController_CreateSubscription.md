[web] POST /api/super/templates/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Templates.TemplatesSubscriptionController.CreateSubscription)  [L57–L74] status=201 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls TenantRepository.WriteTable [L64]
  └─ write Tenant [L64]
    └─ reads_from Tenants
  └─ uses_service TenantRepository
    └─ method WriteTable [L64]
      └─ implementation Dataverse.Tenants.Data.TenantRepository.WriteTable [L15-L180]
  └─ sends_request CreateOrUpdateSubscriptionCommand -> CreateOrUpdateSubscriptionCommandHandler [L71]
    └─ handled_by Dataverse.ApplicationService.Commands.Subscriptions.CreateOrUpdateSubscriptionCommandHandler.Handle [L40–L86]
      └─ calls TenantRepository.WriteTable [L55]
      └─ uses_service IControlledRepository<DocumentStore> (Scoped (inferred))
        └─ method ReadQuery [L79]
          └─ implementation Dataverse.Data.Repository.Documents.DocumentStoreRepository.ReadQuery
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L74]
          └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Tenant writes=1 reads=0
    └─ services 1
      └─ TenantRepository
    └─ requests 1
      └─ CreateOrUpdateSubscriptionCommand
    └─ handlers 1
      └─ CreateOrUpdateSubscriptionCommandHandler

