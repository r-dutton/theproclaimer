[web] DELETE /api/accounting/reports/images/{imageId}  (Cirrus.Api.Controllers.Accounting.Reports.Images.ImagesController.Delete)  [L63–L68] status=200 [auth=Authentication.UserPolicy]
  └─ calls ImageRepository (methods: Remove,WriteQuery) [L67]
  └─ delete Image [L67]
    └─ reads_from Images
  └─ write Image [L66]
    └─ reads_from Images
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ Image writes=2 reads=0

