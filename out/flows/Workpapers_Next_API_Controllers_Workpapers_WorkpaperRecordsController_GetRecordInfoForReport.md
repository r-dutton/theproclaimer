[web] GET /api/workpaper-records/for-report  (Workpapers.Next.API.Controllers.Workpapers.WorkpaperRecordsController.GetRecordInfoForReport)  [L146–L151] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetWorkpaperRecordInfoForReportQuery [L150]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.WorkpaperRecords.GetWorkpaperRecordInfoForReportQueryHandler.Handle [L32–L100]
      └─ maps_to WorkpaperRecordForReportDto [L55]
        └─ automapper.registration WorkpapersMappingProfile (WorkpaperRecord->WorkpaperRecordForReportDto) [L539]
      └─ uses_service IControlledRepository<WorkpaperRecord>
        └─ method ReadQuery [L55]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L58]
          └─ ... (no implementation details available)
      └─ uses_service MatterCountQueryProcessor
        └─ method ProcessMatterCounts [L72]
          └─ ... (no implementation details available)
      └─ uses_service RequestProcessor
        └─ method ProcessAsync [L53]
          └─ implementation Workpapers.Next.Services.Features.Requests.RequestProcessor.ProcessAsync [L9-L32]
            └─ constructs RequestProcessorWrapper<TRequest,TResult>
            └─ resolves IPipelineBehavior<TRequest,TResult> chain
            └─ invokes IAsyncRequestHandler<TRequest,TResult>.Handle

