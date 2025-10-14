[web] POST /api/offices/  (Cirrus.Api.Controllers.Firm.OfficesController.Create)  [L79–L85] [auth=user,admin]
  └─ calls OfficeRepository.Add [L83]
  └─ writes_to Office [L83]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method Add [L83]

