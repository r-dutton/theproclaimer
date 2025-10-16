[web] PUT /api/dataverse/synchronise-binder-status  (Workpapers.Next.API.Controllers.DataverseController.UpdateBinderStatuses)  [L326–L332] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ sends_request SynchroniseBinderStatusCommand -> SynchroniseBinderStatusCommandHandler [L331]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Dataverse.SynchroniseBinderStatusCommandHandler.Handle [L28–L85]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L78]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service UnitOfWork
        └─ method Table [L71]
          └─ implementation UnitOfWork.Table
      └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
        └─ method ReadQuery [L49]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ requests 1
      └─ SynchroniseBinderStatusCommand
    └─ handlers 1
      └─ SynchroniseBinderStatusCommandHandler

