using TestDemo.WholesaleInputs.Dtos;
using TestDemo.WholesaleInputs;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.WholesaleAssumption;
using TestDemo.Wholesale.Dtos;
using TestDemo.Wholesale;
using TestDemo.EclShared.Dtos;
using TestDemo.EclShared;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using AutoMapper;
using TestDemo.Auditing.Dto;
using TestDemo.Authorization.Accounts.Dto;
using TestDemo.Authorization.Permissions.Dto;
using TestDemo.Authorization.Roles;
using TestDemo.Authorization.Roles.Dto;
using TestDemo.Authorization.Users;
using TestDemo.Authorization.Users.Dto;
using TestDemo.Authorization.Users.Importing.Dto;
using TestDemo.Authorization.Users.Profile.Dto;
using TestDemo.Chat;
using TestDemo.Chat.Dto;
using TestDemo.Editions;
using TestDemo.Editions.Dto;
using TestDemo.Friendships;
using TestDemo.Friendships.Cache;
using TestDemo.Friendships.Dto;
using TestDemo.Localization.Dto;
using TestDemo.MultiTenancy;
using TestDemo.MultiTenancy.Dto;
using TestDemo.MultiTenancy.HostDashboard.Dto;
using TestDemo.MultiTenancy.Payments;
using TestDemo.MultiTenancy.Payments.Dto;
using TestDemo.Notifications.Dto;
using TestDemo.Organizations.Dto;
using TestDemo.Sessions.Dto;

namespace TestDemo
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
           configuration.CreateMap<CreateOrEditWholesaleEclDataPaymentScheduleDto, WholesaleEclDataPaymentSchedule>().ReverseMap();
           configuration.CreateMap<WholesaleEclDataPaymentScheduleDto, WholesaleEclDataPaymentSchedule>().ReverseMap();
           configuration.CreateMap<CreateOrEditWholesaleEclDataLoanBookDto, WholesaleEclDataLoanBook>().ReverseMap();
           configuration.CreateMap<WholesaleEclDataLoanBookDto, WholesaleEclDataLoanBook>().ReverseMap();
           configuration.CreateMap<CreateOrEditWholesaleEclUploadApprovalDto, WholesaleEclUploadApproval>().ReverseMap();
           configuration.CreateMap<WholesaleEclUploadApprovalDto, WholesaleEclUploadApproval>().ReverseMap();
           configuration.CreateMap<CreateOrEditWholesaleEclUploadDto, WholesaleEclUpload>().ReverseMap();
           configuration.CreateMap<WholesaleEclUploadDto, WholesaleEclUpload>().ReverseMap();
           configuration.CreateMap<CreateOrEditLgdAssumptionUnsecuredRecoveryDto, LgdInputAssumption>().ReverseMap();
           configuration.CreateMap<LgdAssumptionUnsecuredRecoveryDto, LgdInputAssumption>().ReverseMap();
           configuration.CreateMap<CreateOrEditWholesaleEadInputAssumptionDto, WholesaleEclEadInputAssumption>().ReverseMap();
           configuration.CreateMap<WholesaleEadInputAssumptionDto, WholesaleEclEadInputAssumption>().ReverseMap();
           configuration.CreateMap<CreateOrEditWholesaleEclApprovalDto, WholesaleEclApproval>().ReverseMap();
           configuration.CreateMap<WholesaleEclApprovalDto, WholesaleEclApproval>().ReverseMap();
           configuration.CreateMap<CreateOrEditWholesaleEclAssumptionApprovalDto, WholesaleEclAssumptionApproval>().ReverseMap();
           configuration.CreateMap<WholesaleEclAssumptionApprovalDto, WholesaleEclAssumptionApproval>().ReverseMap();
           configuration.CreateMap<CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto, WholesaleEclPdSnPCummulativeDefaultRate>().ReverseMap();
           configuration.CreateMap<WholesaleEclPdSnPCummulativeDefaultRatesDto, WholesaleEclPdSnPCummulativeDefaultRate>().ReverseMap();
           configuration.CreateMap<CreateOrEditWholesaleEclPdAssumption12MonthsDto, WholesaleEclPdAssumption12Month>().ReverseMap();
           configuration.CreateMap<WholesaleEclPdAssumption12MonthsDto, WholesaleEclPdAssumption12Month>().ReverseMap();
           configuration.CreateMap<CreateOrEditWholesaleEclLgdAssumptionDto, WholesaleEclLgdAssumption>().ReverseMap();
           configuration.CreateMap<WholesaleEclLgdAssumptionDto, WholesaleEclLgdAssumption>().ReverseMap();
           configuration.CreateMap<CreateOrEditWholesaleEadInputAssumptionDto, WholesaleEclEadInputAssumption>().ReverseMap();
           configuration.CreateMap<WholesaleEadInputAssumptionDto, WholesaleEclEadInputAssumption>().ReverseMap();
           configuration.CreateMap<CreateOrEditWholesaleEclAssumptionDto, WholesaleEclAssumption>().ReverseMap();
           configuration.CreateMap<WholesaleEclAssumptionDto, WholesaleEclAssumption>().ReverseMap();
           configuration.CreateMap<CreateOrEditWholesaleEclDto, WholesaleEcl>().ReverseMap();
           configuration.CreateMap<WholesaleEclDto, WholesaleEcl>().ReverseMap();
           configuration.CreateMap<CreateOrEditAssumptionDto, Assumption>().ReverseMap();
           configuration.CreateMap<AssumptionDto, Assumption>().ReverseMap();
           configuration.CreateMap<CreateOrEditPdInputSnPCummulativeDefaultRateDto, PdInputSnPCummulativeDefaultRate>().ReverseMap();
           configuration.CreateMap<PdInputSnPCummulativeDefaultRateDto, PdInputSnPCummulativeDefaultRate>().ReverseMap();
           configuration.CreateMap<CreateOrEditPdInputAssumption12MonthDto, PdInputAssumption12Month>().ReverseMap();
           configuration.CreateMap<PdInputAssumption12MonthDto, PdInputAssumption12Month>().ReverseMap();
           configuration.CreateMap<CreateOrEditLgdAssumptionUnsecuredRecoveryDto, LgdInputAssumption>().ReverseMap();
           configuration.CreateMap<LgdAssumptionUnsecuredRecoveryDto, LgdInputAssumption>().ReverseMap();
           configuration.CreateMap<CreateOrEditEadInputAssumptionDto, EadInputAssumption>().ReverseMap();
           configuration.CreateMap<EadInputAssumptionDto, EadInputAssumption>().ReverseMap();
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();


            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
        }
    }
}