[web] GET /api/reportsettings/policies/  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.GetPolicies)  [L78–L92]
  └─ maps_to OrderedPolicy [L91]
    └─ converts_to OrderedPolicy [L17]
  └─ uses_service IMapper
    └─ method Map [L91]
  └─ uses_service UserService
    └─ method IsSuperUser [L82]
  └─ sends_request GetFirmReportSettingQuery [L88]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]

