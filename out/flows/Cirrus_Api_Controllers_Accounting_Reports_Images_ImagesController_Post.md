[web] POST /api/accounting/reports/images  (Cirrus.Api.Controllers.Accounting.Reports.Images.ImagesController.Post)  [L46–L54] [auth=Authentication.UserPolicy]
  └─ calls ImageRepository.Add [L53]
  └─ calls ImageRepository.ReadQuery [L51]
  └─ queries Image [L51]
    └─ reads_from Images
  └─ writes_to Image [L53]
    └─ reads_from Images
  └─ uses_service IControlledRepository<Image>
    └─ method ReadQuery [L51]

