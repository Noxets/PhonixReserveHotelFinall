using Hamkare.Infrastructure;
using Hamkare.Utility.Attributes.Core;
using Hamkare.Utility.Settings;
using System.Reflection;
using Hamkare.Common.Interface.Repositories;
using Hamkare.Service.Services.Generic;
using Setting=Hamkare.Common.Entities.General.Setting;

namespace Hamkare.Service.Services.General;

public class SettingService(IRootRepository<Setting, ApplicationDbContext> repository) : RootService<Setting, ApplicationDbContext>(repository)
{
    private readonly IRootRepository<Setting, ApplicationDbContext> _repository = repository;
    public async Task UpdateSetting(string propertyName = null, CancellationToken cancellationToken = default)
    {
        var allItems = await _repository.GetAllAsync(cancellationToken);

        var groupedSettings = allItems
            .GroupBy(x => x.Category)
            .ToDictionary(g => g.Key, g => g.ToDictionary(item => item.Key, item => item.Value));

        var settingsType = typeof(Settings);

        if (string.IsNullOrEmpty(propertyName))
        {
            foreach (var category in groupedSettings.Keys)
            {
                propertyName = $"{category}Settings";
                var property = settingsType.GetProperty(propertyName);

                if (property == null)
                    continue;

                var subSettings = Activator.CreateInstance(property.PropertyType);

                foreach (var settingKey in groupedSettings[category].Keys)
                {
                    var subProperty = property.PropertyType.GetProperty(settingKey);

                    if (subProperty == null || IsNotUpdateAttributeDefined(subProperty))
                        continue;

                    var value = Convert.ChangeType(groupedSettings[category][settingKey], subProperty.PropertyType);
                    subProperty.SetValue(subSettings, value);
                }

                property.SetValue(null, subSettings);
            }
        }
        else
        {
            var property = settingsType.GetProperty(propertyName);
            var category = propertyName.Replace("Settings", "");
            if (property == null)
                return;

            var subSettings = Activator.CreateInstance(property.PropertyType);

            foreach (var settingKey in groupedSettings[category].Keys)
            {
                var subProperty = property.PropertyType.GetProperty(settingKey);

                if (subProperty == null || IsNotUpdateAttributeDefined(subProperty))
                    continue;

                var value = Convert.ChangeType(groupedSettings[category][settingKey], subProperty.PropertyType);
                subProperty.SetValue(subSettings, value);
            }

            property.SetValue(null, subSettings);
        }
    }

    public async Task UpdateDatabase(string category = null, CancellationToken cancellationToken = default)
    {
        var allItems = await _repository.GetAllAsync(cancellationToken);
        var settingsType = typeof(Settings);
        
        if (string.IsNullOrEmpty(category))
        {
            foreach (var property in settingsType.GetProperties())
            {
                var subSettings = Activator.CreateInstance(property.PropertyType);
                category = property.Name.Replace("Settings", "");
                foreach (var settingKey in property.PropertyType.GetProperties())
                {
                    if (IsNotUpdateAttributeDefined(settingKey))
                        continue;

                    var databaseValue = allItems.FirstOrDefault(x => x.Category == category && x.Key == settingKey.Name);

                    if (databaseValue == null)
                    {
                        databaseValue = new Setting
                        {
                            Active = true,
                            Deleted = false,
                            System = false,
                            Category = category,
                            Key = settingKey.Name,
                            Value = settingKey.GetValue(subSettings)?.ToString()
                        };

                        await AddOrUpdateAsync(databaseValue, cancellationToken);
                        continue;
                    }

                    databaseValue.Value = settingKey.GetValue(subSettings)?.ToString();
                    await AddOrUpdateAsync(databaseValue, cancellationToken);
                }
            }
        }
        else
        {
            var property = settingsType.GetProperties().First(x => x.Name == category);
            var subSettings = Activator.CreateInstance(property.PropertyType);
            var propertyName = property.Name.Replace("Settings", "");
            foreach (var settingKey in property.PropertyType.GetProperties())
            {
                if (IsNotUpdateAttributeDefined(settingKey))
                    continue;

                var databaseValue = allItems.FirstOrDefault(x => x.Category == propertyName && x.Key == settingKey.Name);

                if (databaseValue == null)
                {
                    databaseValue = new Setting
                    {
                        Active = true,
                        Deleted = false,
                        System = false,
                        Category = propertyName,
                        Key = settingKey.Name,
                        Value = settingKey.GetValue(subSettings)?.ToString()
                    };

                    await AddOrUpdateAsync(databaseValue, cancellationToken);
                    continue;
                }

                databaseValue.Value = settingKey.GetValue(subSettings)?.ToString();
                await AddOrUpdateAsync(databaseValue, cancellationToken);
            }
        }


        await UpdateSetting(category, cancellationToken: cancellationToken);
    }
    
    private bool IsNotUpdateAttributeDefined(PropertyInfo property)
    {
        return Attribute.IsDefined(property, typeof(NotUpdateSettingAttribute));
    }
}