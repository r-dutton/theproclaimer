[web] PUT /api/workpaper-records/move  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.Move)  [L357–L363] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request MoveWorkpaperRecordsCommand -> MoveWorkpaperRecordsCommandHandler [L360]
    └─ handled_by Workpapers.Next.ApplicationService.Commands.Workpapers.WorkpaperRecords.MoveWorkpaperRecordsCommandHandler.Handle [L37–L208]
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L79]
          └─ resolves_request Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync
          └─ +1 additional_requests elided
      └─ uses_service IControlledRepository<Binder> (Scoped (inferred))
        └─ method ReadQuery [L77]
          └─ implementation Workpapers.Next.Data.Repository.BinderRepository.ReadQuery
      └─ uses_service IControlledRepository<WorkpaperRecord> (Scoped (inferred))
        └─ method ReadQuery [L59]
          └─ implementation Workpapers.Next.Data.Repository.Workpapers.WorkpaperRecordRepository.ReadQuery
  └─ impact_summary
    └─ requests 1
      └─ MoveWorkpaperRecordsCommand
    └─ handlers 1
      └─ MoveWorkpaperRecordsCommandHandler

