public class CardProperty {
    public int Value { get; set; }
}

public class HealthProperty : CardProperty { }
public class ManaProperty : CardProperty { }
public class AttackProperty : CardProperty { }