[web] GET /api/binders  (Workpapers.Next.API.Controllers.Workpapers.BindersController.GetCreateBinderParams)  [L541–L566] status=200
  └─ calls BinderTemplateRepository.ReadQuery [L543]
  └─ query BinderTemplate [L543]
    └─ reads_from BinderTemplates
  └─ uses_service IControlledRepository<BinderTemplate>
    └─ method ReadQuery [L543]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<RecordStatus> (AddScoped)
    └─ method WriteQuery [L556]
      └─ ... (no implementation details available)
  └─ uses_service UserService
    └─ method GetUser [L565]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUser [L20-L295]

