[web] GET /api/ui/trials/valid-email  (Dataverse.Api.Controllers.UI.Trial.TrialsController.ValidateEmail)  [L32–L37] status=200 [AllowAnonymous]
  └─ sends_request ValidateTrialEmailQuery -> ValidateTrialEmailQueryHandler [L36]
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Trials.ValidateTrialEmailQueryHandler.Handle [L37–L134]
      └─ calls TenantRepository (methods: ReadTable,WriteTable) [L127]
      └─ uses_service List<string>
        └─ method Any [L118]
          └─ implementation Cirrus.Api.Controllers.Accounting.Reports.PublishedReportsController.List.Any [L60-L77]
            └─ calls PublishedReportBatchRepository.ReadQuery [L66]
            └─ uses_service IRequestProcessor (InstancePerDependency)
              └─ method ProcessAsync [L70]
                └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
                  └─ constructs RequestProcessorWrapper<CanIAccessFileQuery,Unit>
                  └─ resolves IPipelineBehavior<CanIAccessFileQuery,Unit> chain
                  └─ invokes IAsyncRequestHandler<CanIAccessFileQuery,Unit>.Handle
                  └─ dispatches CanIAccessFileQuery [L70]
            └─ uses_service IControlledRepository<PublishedReportBatch> (Scoped (inferred))
              └─ method ReadQuery [L66]
                └─ implementation Cirrus.Data.Repository.Accounting.Report.PublishedReportBatchRepository.ReadQuery
            └─ query PublishedReportBatch [L66]
            └─ dispatches CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70]
            └─ sends_request CanIAccessFileQuery -> CanIAccessFileQueryHandler [L70] ... (reused)
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ PublishedReportBatch writes=0 reads=1
    └─ requests 2
      └─ CanIAccessFileQuery
      └─ ValidateTrialEmailQuery
    └─ handlers 2
      └─ CanIAccessFileQueryHandler
      └─ ValidateTrialEmailQueryHandler

