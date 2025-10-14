[web] GET /api/reportsettings/policies/withparagraphs  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.GetPoliciesWithParagraphs)  [L94–L108]
  └─ maps_to OrderedPolicy [L107]
    └─ converts_to OrderedPolicy [L17]
  └─ uses_service IMapper
    └─ method Map [L107]
  └─ uses_service UserService
    └─ method IsSuperUser [L98]
  └─ sends_request GetFirmReportSettingQuery [L104]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]

