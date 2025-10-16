[web] PUT /api/super/workflow/{tenantId}/subscription  (Dataverse.Api.Controllers.Super.Workflow.WorkflowsSubscriptionController.UpdateSubscription)  [L76–L84] status=200 [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ sends_request CreateOrUpdateSubscriptionCommand -> CreateOrUpdateSubscriptionCommandHandler [L83]
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
    └─ requests 1
      └─ CreateOrUpdateSubscriptionCommand
    └─ handlers 1
      └─ CreateOrUpdateSubscriptionCommandHandler

