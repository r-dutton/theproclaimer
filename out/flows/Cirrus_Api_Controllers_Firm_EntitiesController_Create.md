[web] POST /api/entities/  (Cirrus.Api.Controllers.Firm.EntitiesController.Create)  [L94–L106] [auth=user]
  └─ calls EntityRepository.Add [L104]
  └─ calls ClientRepository.WriteQuery [L97]
  └─ writes_to Client [L97]
  └─ writes_to Entity [L104]
  └─ uses_service IControlledRepository<Client>
    └─ method WriteQuery [L97]
  └─ uses_service IControlledRepository<Entity>
    └─ method Add [L104]
  └─ uses_service IFirmSettingsService (AddScoped)
    └─ method GetFirmJurisdictions [L100]

