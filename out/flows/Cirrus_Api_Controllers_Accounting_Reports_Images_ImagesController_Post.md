[web] POST /api/accounting/reports/images  (Cirrus.Api.Controllers.Accounting.Reports.Images.ImagesController.Post)  [L46–L54] status=201 [auth=Authentication.UserPolicy]
  └─ calls ImageRepository.Add [L53]
  └─ calls ImageRepository.ReadQuery [L51]
  └─ insert Image [L53]
    └─ reads_from Images
  └─ query Image [L51]
    └─ reads_from Images
  └─ uses_service IControlledRepository<Image>
    └─ method ReadQuery [L51]
      └─ ... (no implementation details available)

