[web] GET /api/reportsettings/policies/  (Workpapers.Next.API.Controllers.Reportance.PoliciesController.GetPolicies)  [L78–L92] status=200
  └─ maps_to OrderedPolicy [L91]
    └─ converts_to OrderedPolicy [L17]
  └─ uses_service IMapper
    └─ method Map [L91]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method IsSuperUser [L82]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.IsSuperUser [L20-L295]
  └─ sends_request GetFirmReportSettingQuery [L88]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.ReportanceDesktop.GetFirmReportSettingQueryHandler.Handle [L10–L49]
      └─ uses_service UnitOfWork
        └─ method Table [L24]
          └─ ... (no implementation details available)

