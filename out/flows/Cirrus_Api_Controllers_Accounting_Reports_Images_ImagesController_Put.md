[web] PUT /api/accounting/reports/images/{imageId}  (Cirrus.Api.Controllers.Accounting.Reports.Images.ImagesController.Put)  [L56–L61] status=200 [auth=Authentication.UserPolicy]
  └─ calls ImageRepository.WriteQuery [L59]
  └─ write Image [L59]
    └─ reads_from Images
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Image writes=1 reads=0

