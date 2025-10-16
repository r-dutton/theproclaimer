[web] PUT /api/workpaper-records/move  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.Move)  [L357–L363] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request MoveWorkpaperRecordsCommand [L360]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.MoveWorkpaperRecordsCommandHandler.Handle [L37–L208]
      └─ uses_service IControlledRepository<Binder>
        └─ method ReadQuery [L77]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L59]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L79]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

