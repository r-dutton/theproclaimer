[web] POST /api/offices/  (Cirrus.Api.Controllers.Firm.OfficesController.Create)  [L79–L85] status=201 [auth=user,admin]
  └─ calls OfficeRepository.Add [L83]
  └─ insert Office [L83]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method Add [L83]
      └─ ... (no implementation details available)

