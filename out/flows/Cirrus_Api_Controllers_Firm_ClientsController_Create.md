[web] POST /api/clients/  (Cirrus.Api.Controllers.Firm.ClientsController.Create)  [L162–L174] [auth=user]
  └─ calls ClientRepository.Add [L172]
  └─ writes_to Client [L172]
  └─ uses_service IControlledRepository<Client>
    └─ method Add [L172]
  └─ uses_service IFirmSettingsService (AddScoped)
    └─ method GetCurrentSettings [L165]
  └─ uses_service IUserService (InstancePerLifetimeScope)
    └─ method GetUser [L166]

