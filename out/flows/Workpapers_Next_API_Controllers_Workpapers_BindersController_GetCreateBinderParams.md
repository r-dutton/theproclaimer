[web] GET /api/binders  (Workpapers.Next.API.Controllers.Workpapers.BindersController.GetCreateBinderParams)  [L541–L566]
  └─ calls BinderTemplateRepository.ReadQuery [L543]
  └─ queries BinderTemplate [L543]
    └─ reads_from BinderTemplates
  └─ uses_service IControlledRepository<BinderTemplate>
    └─ method ReadQuery [L543]
  └─ uses_service IControlledRepository<RecordStatus> (AddScoped)
    └─ method WriteQuery [L556]
  └─ uses_service UserService
    └─ method GetUser [L565]

