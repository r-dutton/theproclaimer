[web] PUT /api/offices/{id}  (Cirrus.Api.Controllers.Firm.OfficesController.Update)  [L90–L96] status=200 [auth=user,admin]
  └─ calls OfficeRepository.WriteQuery [L93]
  └─ write Office [L93]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method WriteQuery [L93]
      └─ ... (no implementation details available)

