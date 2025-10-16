[web] GET /api/accounting/reports/notes/disclosures/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.Get)  [L50–L79] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DisclosureVariantDto (var dto) [L71]
  └─ calls DisclosureVariantRepository.ReadQuery [L56]
  └─ calls ReportContentRepository.LoadReadProperties [L68]
  └─ query DisclosureVariant [L56]
    └─ reads_from DisclosureVariants
  └─ uses_service IControlledRepository<DisclosureVariant>
    └─ method ReadQuery [L56]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L71]
      └─ ... (no implementation details available)
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadReadProperties [L68]
      └─ implementation Cirrus.Data.Repository.Accounting.Report.ReportContentRepository.LoadReadProperties [L11-L65]
  └─ sends_request PrepareImagesContentCommand [L72]

