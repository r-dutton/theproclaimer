[web] DELETE /api/offices/{id:Guid}  (Cirrus.Api.Controllers.Firm.OfficesController.Delete)  [L101–L107] [auth=user,admin]
  └─ calls OfficeRepository.Remove [L105]
  └─ calls OfficeRepository.WriteQuery [L104]
  └─ writes_to Office [L105]
    └─ reads_from Offices
  └─ writes_to Office [L104]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method WriteQuery [L104]

