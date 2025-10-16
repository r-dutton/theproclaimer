[web] GET /api/accounting/reports/notes/disclosures/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.Get)  [L50–L79] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DisclosureVariantDto (var dto) [L71]
  └─ calls ReportContentRepository.LoadReadProperties [L68]
  └─ calls DisclosureVariantRepository.ReadQuery [L56]
  └─ query DisclosureVariant [L56]
    └─ reads_from DisclosureVariants
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadReadProperties [L68]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadReadProperties [L11-L65]
  └─ sends_request PrepareImagesContentCommand [L72]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DisclosureVariant writes=0 reads=1
    └─ services 1
      └─ ReportContentRepository
    └─ requests 1
      └─ PrepareImagesContentCommand
    └─ mappings 1
      └─ DisclosureVariantDto

