[web] GET /api/accounting/reports/notes/policies/variants/{id:Guid}  (Cirrus.Api.Controllers.Accounting.Reports.Notes.PolicyVariantsController.Get)  [L46–L64] [auth=Authentication.UserPolicy]
  └─ maps_to PolicyVariantDto (var dto) [L61]
  └─ calls PolicyVariantRepository.ReadQuery [L49]
  └─ calls ReportContentRepository.LoadReadProperties [L58]
  └─ queries PolicyVariant [L49]
    └─ reads_from PolicyVariants
  └─ uses_service IControlledRepository<PolicyVariant>
    └─ method ReadQuery [L49]
  └─ uses_service IMapper
    └─ method Map [L61]
  └─ uses_service ReportContentRepository (InstancePerLifetimeScope)
    └─ method LoadReadProperties [L58]
  └─ sends_request PrepareImagesContentCommand [L62]

