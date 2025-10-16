[web] POST /api/clients/  (Cirrus.Api.Controllers.Firm.ClientsController.Create)  [L162–L174] status=201 [auth=user]
  └─ calls ClientRepository.Add [L172]
  └─ insert Client [L172]
  └─ uses_service IControlledRepository<Client>
    └─ method Add [L172]
      └─ ... (no implementation details available)
  └─ uses_service IFirmSettingsService (AddScoped)
    └─ method GetCurrentSettings [L165]
      └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetCurrentSettings [L15-L112]
  └─ uses_service IUserService (InstancePerLifetimeScope)
    └─ method GetUser [L166]
      └─ implementation IUserService.GetUser [L18-L18]
      └─ ... (no implementation details available)

