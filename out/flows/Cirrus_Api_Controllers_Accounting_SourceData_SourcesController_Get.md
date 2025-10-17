[web] GET /api/accounting/sourcedata/sources/{id}  (Cirrus.Api.Controllers.Accounting.SourceData.SourcesController.Get)  [L55–L64] status=200 [auth=user]
  └─ maps_to UserUltraLightDto [L61]
    └─ automapper.registration CirrusMappingProfile (User->UserUltraLightDto) [L103]
  └─ maps_to SourceDto [L58]
    └─ automapper.registration CirrusMappingProfile (Source->SourceDto) [L216]
  └─ calls UserRepository.ReadQuery [L61]
  └─ calls SourceRepository.ReadQuery [L58]
  └─ query User [L61]
  └─ query Source [L58]
  └─ uses_service ApiService (SingleInstance)
    └─ method GetFeatures [L62]
      └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
  └─ impact_summary
    └─ entities 2 (writes=0, reads=2)
      └─ Source writes=0 reads=1
      └─ User writes=0 reads=1
    └─ services 1
      └─ ApiService
    └─ mappings 2
      └─ SourceDto
      └─ UserUltraLightDto

