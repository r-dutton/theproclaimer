[web] GET /api/accounting/files/  (Cirrus.Api.Controllers.Accounting.FilesController.Search)  [L55–L81] [auth=user]
  └─ sends_request FindFilesQuery [L69]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.FindFilesQueryHandler.Handle [L58–L195]
      └─ maps_to UserLightDto [L181]
      └─ uses_service IControlledRepository<Dataset>
        └─ method ReadQuery [L172]
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L91]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L125]
      └─ uses_service IFirmSettingsService (AddScoped)
        └─ method GetCurrentSettings [L122]
      └─ uses_service IMapper
        └─ method Map [L181]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L87]

