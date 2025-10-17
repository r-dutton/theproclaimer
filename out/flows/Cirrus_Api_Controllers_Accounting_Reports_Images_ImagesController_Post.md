[web] POST /api/accounting/reports/images  (Cirrus.Api.Controllers.Accounting.Reports.Images.ImagesController.Post)  [L46–L54] status=201 [auth=Authentication.UserPolicy]
  └─ calls ImageRepository (methods: Add,ReadQuery) [L53]
  └─ insert Image [L53]
    └─ reads_from Images
  └─ query Image [L51]
    └─ reads_from Images
  └─ impact_summary
    └─ entities 1 (writes=1, reads=1)
      └─ Image writes=1 reads=1

