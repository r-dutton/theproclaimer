[web] DELETE /api/offices/{id:Guid}  (Cirrus.Api.Controllers.Firm.OfficesController.Delete)  [L101–L107] status=200 [auth=user,admin]
  └─ calls OfficeRepository.Remove [L105]
  └─ calls OfficeRepository.WriteQuery [L104]
  └─ delete Office [L105]
    └─ reads_from Offices
  └─ write Office [L104]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method WriteQuery [L104]
      └─ ... (no implementation details available)

