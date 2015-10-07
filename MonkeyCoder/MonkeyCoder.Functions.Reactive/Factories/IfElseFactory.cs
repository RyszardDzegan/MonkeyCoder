namespace MonkeyCoder.Functions.Reactive
{
    internal class IfElseFactory : TernaryTypeSafeFactory<IfElse, IBoolean, INumber, INumber>
    {
        protected override bool AcceptsAll(IEvaluable a, IEvaluable b, IEvaluable c) =>
            !b.Equals(c) && !b.Evaluate().Equals(c.Evaluate());
    }
}
