[web] DELETE /api/accounting/reports/images/{imageId}  (Cirrus.Api.Controllers.Accounting.Reports.Images.ImagesController.Delete)  [L63–L68] [auth=Authentication.UserPolicy]
  └─ calls ImageRepository.Remove [L67]
  └─ calls ImageRepository.WriteQuery [L66]
  └─ writes_to Image [L67]
    └─ reads_from Images
  └─ writes_to Image [L66]
    └─ reads_from Images
  └─ uses_service IControlledRepository<Image>
    └─ method WriteQuery [L66]

