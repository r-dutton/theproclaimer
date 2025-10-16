[web] DELETE /api/reportsettings/reset  (Workpapers.Next.API.Controllers.Reportance.FirmReportSettingsController.ResetToFactoryDefaults)  [L99–L110] status=200 [auth=AuthorizationPolicies.Administrator]
  └─ uses_service UnitOfWork
    └─ method Table [L103]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetFirmId [L103]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetFirmId [L20-L295]

