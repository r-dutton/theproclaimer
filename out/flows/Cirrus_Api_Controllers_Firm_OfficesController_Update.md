[web] PUT /api/offices/{id}  (Cirrus.Api.Controllers.Firm.OfficesController.Update)  [L90–L96] [auth=user,admin]
  └─ calls OfficeRepository.WriteQuery [L93]
  └─ writes_to Office [L93]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method WriteQuery [L93]

