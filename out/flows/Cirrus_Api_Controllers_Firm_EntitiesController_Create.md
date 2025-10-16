[web] POST /api/entities/  (Cirrus.Api.Controllers.Firm.EntitiesController.Create)  [L94–L106] status=201 [auth=user]
  └─ calls EntityRepository.Add [L104]
  └─ calls ClientRepository.WriteQuery [L97]
  └─ write Client [L97]
  └─ insert Entity [L104]
  └─ uses_service IControlledRepository<Client>
    └─ method WriteQuery [L97]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<Entity>
    └─ method Add [L104]
      └─ ... (no implementation details available)
  └─ uses_service IFirmSettingsService (AddScoped)
    └─ method GetFirmJurisdictions [L100]
      └─ implementation Cirrus.ApplicationService.Firm.FirmSettingsService.GetFirmJurisdictions [L15-L112]

