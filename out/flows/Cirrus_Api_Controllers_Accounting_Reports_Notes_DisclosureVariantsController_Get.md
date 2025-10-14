[web] GET /api/accounting/reports/notes/disclosures/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.DisclosureVariantsController.Get)  [L50–L79] [auth=Authentication.UserPolicy]
  └─ maps_to DisclosureVariantDto (var dto) [L71]
  └─ calls DisclosureVariantRepository.ReadQuery [L56]
  └─ calls ReportContentRepository.LoadReadProperties [L68]
  └─ queries DisclosureVariant [L56]
    └─ reads_from DisclosureVariants
  └─ uses_service IControlledRepository<DisclosureVariant>
    └─ method ReadQuery [L56]
  └─ uses_service IMapper
    └─ method Map [L71]
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadReadProperties [L68]
  └─ sends_request PrepareImagesContentCommand [L72]

