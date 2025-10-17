[web] GET /api/accounting/reports/notes/policies/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.Get)  [L46–L64] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to PolicyVariantDto (var dto) [L61]
  └─ calls ReportContentRepository.LoadReadProperties [L58]
  └─ calls PolicyVariantRepository.ReadQuery [L49]
  └─ query PolicyVariant [L49]
    └─ reads_from PolicyVariants
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadReadProperties [L58]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadReadProperties [L11-L65]
  └─ sends_request PrepareImagesContentCommand [L62]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ PolicyVariant writes=0 reads=1
    └─ services 1
      └─ ReportContentRepository
    └─ requests 1
      └─ PrepareImagesContentCommand
    └─ mappings 1
      └─ PolicyVariantDto

