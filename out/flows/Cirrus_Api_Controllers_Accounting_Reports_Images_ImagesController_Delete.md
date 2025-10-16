[web] DELETE /api/accounting/reports/images/{imageId}  (Cirrus.Api.Controllers.Accounting.Reports.Images.ImagesController.Delete)  [L63–L68] status=200 [auth=Authentication.UserPolicy]
  └─ calls ImageRepository.Remove [L67]
  └─ calls ImageRepository.WriteQuery [L66]
  └─ delete Image [L67]
    └─ reads_from Images
  └─ write Image [L66]
    └─ reads_from Images
  └─ uses_service IControlledRepository<Image>
    └─ method WriteQuery [L66]
      └─ ... (no implementation details available)

