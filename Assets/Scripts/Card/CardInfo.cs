using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class CardInfo {
    public ManaProperty Mana { get; private set; } = new ManaProperty();
    public HealthProperty Health { get; private set; } = new HealthProperty();
    public AttackProperty Attack { get; private set; } = new AttackProperty();
    private Dictionary<Type, CardProperty> _typeToProperty = new Dictionary<Type, CardProperty>();

    public CardInfo() {
        _typeToProperty[typeof(ManaProperty)] = Mana;
        _typeToProperty[typeof(AttackProperty)] = Attack;
        _typeToProperty[typeof(HealthProperty)] = Health;
    }

    public void SetProperyValue(Type property, int value) {
        Debug.Assert(property.IsSubclassOf(typeof(CardProperty)));
        _typeToProperty[property].Value = value;
    }

    public List<CardProperty> GetProperties() {
        return _typeToProperty.Values.ToList();
    }

}
