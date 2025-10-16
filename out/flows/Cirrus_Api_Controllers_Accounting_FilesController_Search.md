[web] GET /api/accounting/files/  (Cirrus.Api.Controllers.Accounting.FilesController.Search)  [L55–L81] status=200 [auth=user]
  └─ sends_request FindFilesQuery [L69]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindFilesQueryHandler.Handle [L58–L195]
      └─ maps_to UserLightDto [L181]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L172]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L91]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L125]
          └─ ... (no implementation details available)
      └─ uses_service IFirmSettingsService (AddScoped)
        └─ method GetCurrentSettings [L122]
          └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings [L15-L112]
      └─ uses_service IMapper
        └─ method Map [L181]
          └─ ... (no implementation details available)
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L87]
          └─ implementation IUserService.GetUserId [L18-L18]
          └─ ... (no implementation details available)

